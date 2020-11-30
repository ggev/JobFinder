import React, { Component } from 'react';
import './style.css'

import CoverImage from '../assets/images/cover.jpg'
import PlusImage from '../assets/images/plus.png'
import MinusImage from '../assets/images/minus.png'
import LocationImage from '../assets/images/pin.png'
import ClockImage from '../assets/images/clock.png'
import BookmarkImage from '../assets/images/bookmark.png'
import BriefcaseImage from '../assets/images/briefcase.png'
import Select from "../components/select/Select";
import { categoryListForFilterBox, locationList, sortByList } from "./service";
import Modal from "../components/modal/Modal.js";
import ModalDetails from "../components/modal-detail/Modal-detail";
import Pagination from '../components/pagination/pagination';

import CategoryController from '../platform/api/category';
import getImage from '../platform/service/uploadImage';
import JobController from '../platform/api/jobs';
export class Home extends Component {

    state = {
        isOpenModal: false,
        selectedItem: null,
        formSearch: {
            category: null,
            location: null
        },
        isActiveSearchBox: null,
        filterReq: [],
        categoryList: [],
        jobsListReqM: {
            pageNumber: 1,
            pageSize: 10,
            descending: false,
            search: '',
            categoryIds: [],
            locations: [],
            employmentTypes: [],
            bookmarked: null,
            appliedForJob: null,
            orderBy: 1
        },
        bookmarkModel: {
            id: null,
            mark: false
        },
        jobList: {
            data: [],
            pageCount: null,
            totalCount: null
        }
    }

    componentDidMount() {
        this.getCategoryList();
        this.getJobsList();
    }

    async getCategoryList() {
        const res = await CategoryController.getList();
        if (res && res.success) {
            this.setState({ categoryList: res.data.map(x => ({ name: x.name, value: x.id })) });
        }
    }

    async getJobsList() {
        const { jobsListReqM, jobList, selectedItem  } = this.state;
        const res = await JobController.getList(jobsListReqM);
        if (res && res.success) {
            this.setState({ jobList: res.data });
            if (!!selectedItem) {
                const result = await JobController.detail(selectedItem.id);
                if (result && result.success) {
                    this.setState({
                        selectedItem: result.data
                    })
                }
            }
        }
    }
    async checkBookmark(e, id, bookmark) {
        e.stopPropagation();
        const body = {
            id: id,
            mark: bookmark
        }
        const res = await JobController.bookmark(body);
        if (res && res.success) {
            this.getJobsList();
        }

    }
    async applyJob(e, id) {
        e.stopPropagation();
        const res = await JobController.apply({ id });
        if (res && res.success) {
            this.getJobsList();
        }
    }
    changeCategory = (options) => {
        const { jobsListReqM } = this.state;
        const index = jobsListReqM.categoryIds.findIndex(x => x === options.value);
        if (index > -1) {
            jobsListReqM.categoryIds.splice(index, 1);
        } else jobsListReqM.categoryIds.push(options.value);
        this.setState({ jobsListReqM });
        this.getJobsList();
    }

    changeLocation = (options) => {
        const { formSearch } = this.state
        formSearch.location = options.value;
        this.setState({ formSearch })
    }

    openFilterBoxes = (value) => {
        if (value !== this.state.isActiveSearchBox) {

            this.setState({
                isActiveSearchBox: value
            })
        } else {
            this.setState({
                isActiveSearchBox: null
            })
        }
    }
    
    changeFilter = (itemValue) => {
        const { jobsListReqM } = this.state
        const index = jobsListReqM.categoryIds.indexOf(itemValue);
        if (index >= 0) {
            jobsListReqM.categoryIds.splice(index, 1);
        } else {
            jobsListReqM.categoryIds.push(itemValue);
        }
        this.setState({ jobsListReqM });
        this.getJobsList();
    }
    changeenploymentType = (itemValue) => {
        const { jobsListReqM } = this.state
        const index = jobsListReqM.employmentTypes.indexOf(itemValue);
        if (index >= 0) {
            jobsListReqM.employmentTypes.splice(index, 1);
        } else {
            jobsListReqM.employmentTypes.push(itemValue);
        }
        this.setState({ jobsListReqM });
        this.getJobsList();
    }
    changeLocationType = (itemValue) => {
        const { jobsListReqM } = this.state
        const index = jobsListReqM.locations.indexOf(itemValue);
        if (index >= 0) {
            jobsListReqM.locations.splice(index, 1);
        } else {
            jobsListReqM.locations.push(itemValue);
        }
        this.setState({ jobsListReqM });
        this.getJobsList();
    }
    openModal = async (item) => {
        const res = await JobController.detail(item.id);
        if (res && res.success) {
            document.body.classList.add('P-body-fixed');
            this.setState({
                isOpenModal: true,
                selectedItem: res.data
            })
        }
    }
    closeModal = () => {
        document.body.classList.remove('P-body-fixed')
        this.setState({
            isOpenModal: false
        })
    }
    change = (e) => {
        const { jobsListReqM } = this.state;
        jobsListReqM.search = e.target.value;
        this.setState({ jobsListReqM });
        this.getJobsList();
    }
    changeListSorting = (item) => {
        const { jobsListReqM } = this.state;
        jobsListReqM.orderBy = item.value;
        this.setState({ jobsListReqM });
        this.getJobsList();
    }
    setCategory = (item) => {
        const { jobsListReqM } = this.state;
        jobsListReqM.categoryIds = [item.value];
        this.setState({ jobsListReqM });
        this.getJobsList();
    }
    setLocation = (item) => {
        const { jobsListReqM } = this.state;
        jobsListReqM.locations = [item.value];
        this.setState({ jobsListReqM });
        this.getJobsList();
    }
    getPage = (page) => {
        const { jobsListReqM } = this.state;
        jobsListReqM.pageNumber = page;
        this.setState({ jobsListReqM });
        window.scrollTo(0, 0)

        this.getJobsList();
    }
    render() {
        const { formSearch, isActiveSearchBox, filterReq, categoryList, jobList, jobsListReqM } = this.state
        return (

            <div className='G-container'>
                <div className='P-main-page'>
                    <div className='P-bg-mage' style={{ backgroundImage: `url('${CoverImage}')` }} />
                    <div className='P-header'>
                        <ul className='G-flex G-align-center'>
                            <li>Home</li>
                            <li>Engineer/Architects</li>
                        </ul>
                        <h3>Software Engineer</h3>
                    </div>
                    <div className='P-search-block G-flex G-align-center'>
                        <div className='P-search-box'>
                            <Select placeholder='Job Category'
                                options={categoryList}
                                listKey={'value'}
                                useValue={true}
                                value={formSearch.category}
                                placeholderOpacity={true}
                                isAllList={true}
                                disabled={false}
                                onChange={this.setCategory} />
                        </div>
                        <div className='P-search-box'>
                            <Select placeholder='Job Location'
                                options={locationList}
                                listKey={'value'}
                                useValue={true}
                                value={formSearch.location}
                                placeholderOpacity={true}
                                isAllList={true}
                                disabled={false}
                                onChange={this.setLocation} />
                        </div>
                        <div className='P-input-box'>
                            <label>
                                <input placeholder='Type your key word' onChange={this.change} type="text" />
                            </label>
                        </div>
                        <div className='P-search-btn'>
                            <button onClick={(e) => { e.preventDefault(); this.getJobsList(); }}>SEARCH</button>
                        </div>
                    </div>
                    <div className='P-main-block'>
                        <div className='P-left-path'>
                            <div className='P-filter-box'>
                                <div className='P-filter-box-header' onClick={() => this.openFilterBoxes(1)}>
                                    <h3>Categories</h3>
                                    <span className='P-filter-img'
                                        style={{ backgroundImage: `url('${isActiveSearchBox === 1 ? MinusImage : PlusImage}')` }} />
                                </div>
                                <div className={`P-filter-box-body ${isActiveSearchBox === 1 ? 'P-open-filter-box' : ''}`}>
                                    <ul>
                                        {categoryList.map((item, index) => {
                                            return <li key={index} onClick={() => this.changeFilter(item.value)}>
                                                <div className="P-checkbox-block">
                                                    <label>
                                                        <span className={`${jobsListReqM.categoryIds.includes(item.value) ? 'P-select' : ''}`} />
                                                        <p>{item.name}</p>
                                                    </label>
                                                </div>
                                            </li>
                                        })}
                                    </ul>
                                </div>
                            </div>
                            <div className='P-filter-box'>
                                <div className='P-filter-box-header' onClick={() => this.openFilterBoxes(2)}>
                                    <h3>Employment Type</h3>
                                    <span className='P-filter-img'
                                        style={{ backgroundImage: `url('${isActiveSearchBox === 2 ? MinusImage : PlusImage}')` }} />
                                </div>
                                <div className={`P-filter-box-body ${isActiveSearchBox === 2 ? 'P-open-filter-box' : ''}`}>
                                    <ul>
                                        {categoryListForFilterBox.map((item, index) => {
                                            return <li key={index} onClick={() => this.changeenploymentType(item.value)}>
                                                <div className="P-checkbox-block">
                                                    <label>
                                                        <span className={`${jobsListReqM.employmentTypes.includes(item.value) ? 'P-select' : ''}`} />
                                                        <p>{item.name}</p>
                                                    </label>
                                                </div>
                                            </li>
                                        })}
                                    </ul>
                                </div>
                            </div>
                            <div className='P-filter-box'>
                                <div className='P-filter-box-header' onClick={() => this.openFilterBoxes(3)}>
                                    <h3>Location</h3>
                                    <span className='P-filter-img'
                                        style={{ backgroundImage: `url('${isActiveSearchBox === 3 ? MinusImage : PlusImage}')` }} />
                                </div>
                                <div className={`P-filter-box-body ${isActiveSearchBox === 3 ? 'P-open-filter-box' : ''}`}>
                                    <ul>
                                        {locationList.map((item, index) => {
                                            return <li key={index} onClick={() => this.changeLocationType(item.value)}>
                                                <div className="P-checkbox-block">
                                                    <label>
                                                        <span className={`${jobsListReqM.locations.includes(item.value) ? 'P-select' : ''}`} />
                                                        <p>{item.name}</p>
                                                    </label>
                                                </div>
                                            </li>
                                        })}
                                    </ul>
                                </div>
                            </div>


                        </div>
                        <div className='P-right-path'>
                            <div className='P-result-header G-flex G-align-center G-justify-between'>
                                <h3>Showing {jobList.data.length} of {jobList.totalCount} offers</h3>
                                <div className='P-sort-box G-flex G-align-center'>
                                    <p>Sort by:</p>
                                    <div className='P-sort-select '>
                                        <Select placeholder='Select sort'
                                            options={sortByList}
                                            listKey={'value'}
                                            useValue={true}
                                            value={sortByList.find(x => x.value === jobsListReqM.orderBy)}
                                            placeholderOpacity={true}
                                            isAllList={true}
                                            disabled={false}
                                            onChange={this.changeListSorting} />
                                    </div>
                                </div>
                            </div>
                            <div className='P-result-body'>
                                {!!jobList.data.length && jobList.data.map((item, index) => {
                                    return <div key={index} className='P-result-box G-flex G-align-center G-justify-between'
                                        onClick={() => this.openModal(item)}>
                                        <div className='P-result-left-path G-flex G-align-center'>
                                            <div className='P-result-image' style={{ backgroundImage: `url('${getImage(item.companyLogo)}')` }} />
                                            <div className='P-information'>
                                                <h2>{item.companyName}</h2>
                                                <h3>{item.title}</h3>
                                                <ul>
                                                    <li><span style={{ backgroundImage: `url('${LocationImage}')` }} /> {item.location }, {item.address}</li>
                                                    <li><span style={{ backgroundImage: `url('${ClockImage}')` }} /> {item.employmentType}</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div className='P-result-right-path'>
                                            <div className='P-bookmark-btn'>
                                                <button onClick={(e) => this.checkBookmark(e, item.id, !item.bookmarked)} className={`${item.bookmarked ? 'bookmarked-btn' : ''}`}><span style={{ backgroundImage: `url('${BookmarkImage}')` }} />
                                                    {item.bookmarked ? 'Bookmarked' : 'Bookmark'}
                                                </button>
                                            </div>
                                            <div className='P-apply-btn'>
                                                <button onClick={(e) => this.applyJob(e, item.id)} className={`${item.appliedForJob ? 'applied-btn' : ''}` }><span style={{ backgroundImage: `url('${BriefcaseImage}')` }} />
                                                    {item.appliedForJob ? 'Job was applied' : 'Apply for this job'}

                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                })}
                            </div>
                        </div>
                    </div>
                    <Pagination pageCount={jobList.pageCount} callback={(page) => this.getPage(page)} />
                </div>

                <Modal hangeClass={'P-details-modal-container'} isOpen={this.state.isOpenModal}
                    close={this.closeModal}>
                    <ModalDetails
                        apply={(e, id) => this.applyJob(e, id)}
                        mark={(e, id, mark) => this.checkBookmark(e, id, mark)}
                        close={this.closeModal}
                        item={this.state.selectedItem} />
                </Modal>
            </div>

        )
    }
}
