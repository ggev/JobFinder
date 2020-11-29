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
import { categoryList, categoryListForFilterBox, locationList } from "./service";
import Modal from "../components/modal/Modal.js";
import ModalDetails from "../components/modal-detail/Modal-detail";

export class Home extends Component {

    state = {
        isOpenModal: false,
        selectedItem: null,
        formSearch: {
            category: null,
            location: null
        },
        isActiveSearchBox: null,
        filterReq: []
    }

    changeCategory = (options) => {
        const { formSearch } = this.state
        formSearch.category = options.value;
        this.setState({ formSearch })
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
        const { filterReq } = this.state
        const index = filterReq.indexOf(itemValue);
        if (index >= 0) {
            filterReq.splice(index, 1);
        } else {
            filterReq.push(itemValue);
        }
        this.setState({ filterReq });
    }

    data = [
        {
            title: 'Senior Web developer',
            image: CoverImage,
            location: 'Yerevan, Armenia',
            time: 'Full Time'
        },
        {
            title: 'Senior Web developer',
            image: CoverImage,
            location: 'Yerevan, Armenia',
            time: 'Full Time'
        },
        {
            title: 'Senior Web developer',
            image: CoverImage,
            location: 'Yerevan, Armenia',
            time: 'Full Time'
        },
        {
            title: 'Senior Web developer',
            image: CoverImage,
            location: 'Yerevan, Armenia',
            time: 'Full Time'
        },
        {
            title: 'Senior Web developer',
            image: CoverImage,
            location: 'Yerevan, Armenia',
            time: 'Full Time'
        }
    ]

    openModal = (item) => {
        document.body.classList.add('P-body-fixed')
        this.setState({
            isOpenModal: true,
            selectedItem: item
        })
    }
    closeModal = () => {
        document.body.classList.remove('P-body-fixed')
        this.setState({
            isOpenModal: false
        })
    }

    render() {
        const { formSearch, isActiveSearchBox, filterReq } = this.state
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
                                onChange={this.changeCategory} />
                        </div>
                        <div className='P-search-box'>
                            <Select placeholder='Job Category'
                                options={locationList}
                                listKey={'value'}
                                useValue={true}
                                value={formSearch.location}
                                placeholderOpacity={true}
                                isAllList={true}
                                disabled={false}
                                onChange={this.changeLocation} />
                        </div>
                        <div className='P-input-box'>
                            <label>
                                <input placeholder='Type your key word' type="text" />
                            </label>
                        </div>
                        <div className='P-search-btn'>
                            <button>SEARCH</button>
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
                                        {categoryListForFilterBox.map((item, index) => {
                                            return <li key={index} onClick={() => this.changeFilter(item.value)}>
                                                <div className="P-checkbox-block">
                                                    <label>
                                                        <span className={`${filterReq.includes(item.value) ? 'P-select' : ''}`} />
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
                                            return <li key={index} onClick={() => this.changeFilter(item.value)}>
                                                <div className="P-checkbox-block">
                                                    <label>
                                                        <span className={`${filterReq.includes(item.value) ? 'P-select' : ''}`} />
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
                                        {categoryListForFilterBox.map((item, index) => {
                                            return <li key={index} onClick={() => this.changeFilter(item.value)}>
                                                <div className="P-checkbox-block">
                                                    <label>
                                                        <span className={`${filterReq.includes(item.value) ? 'P-select' : ''}`} />
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
                                <h3>Showing 10 of 26,012 offers</h3>
                                <div className='P-sort-box G-flex G-align-center'>
                                    <p>Sort by:</p>
                                    <div className='P-sort-select '>
                                        <Select placeholder='Select sort'
                                            options={locationList}
                                            listKey={'value'}
                                            useValue={true}
                                            value={formSearch.location}
                                            placeholderOpacity={true}
                                            isAllList={true}
                                            disabled={false}
                                            onChange={this.changeLocation} />
                                    </div>
                                </div>
                            </div>
                            <div className='P-result-body'>
                                {this.data.map((item, index) => {
                                    return <div key={index} className='P-result-box G-flex G-align-center G-justify-between'
                                        onClick={() => this.openModal(item)}>
                                        <div className='P-result-left-path G-flex G-align-center'>
                                            <div className='P-result-image' style={{ backgroundImage: `url('${item.image}')` }} />
                                            <div className='P-information'>
                                                <h3>{item.title}</h3>
                                                <ul>
                                                    <li><span style={{ backgroundImage: `url('${LocationImage}')` }} /> {item.location}</li>
                                                    <li><span style={{ backgroundImage: `url('${ClockImage}')` }} /> {item.time}</li>
                                                </ul>
                                            </div>
                                        </div>
                                        <div className='P-result-right-path'>
                                            <div className='P-bookmark-btn'>
                                                <button><span style={{ backgroundImage: `url('${BookmarkImage}')` }} /> Bookmark</button>
                                            </div>
                                            <div className='P-apply-btn'>
                                                <button><span style={{ backgroundImage: `url('${BriefcaseImage}')` }} /> Apply for this job
                          </button>
                                            </div>
                                        </div>
                                    </div>

                                })}
                            </div>
                        </div>
                    </div>
                </div>

                <Modal hangeClass={'P-details-modal-container'} isOpen={this.state.isOpenModal}
                    close={this.closeModal}>
                    <ModalDetails close={this.closeModal} item={this.state.selectedItem} />
                </Modal>
            </div>

        )
    }
}
