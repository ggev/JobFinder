import React, { Component } from 'react';
import './style.css';
import LocationImage from "../../assets/images/pin.png";
import ClockImage from "../../assets/images/clock.png";
import BookmarkImage from "../../assets/images/bookmark.png";
import CloseImage from "../../assets/images/cancel.png";

import BriefcaseImage from "../../assets/images/briefcase.png";
import getImage from '../../platform/service/uploadImage';

class ModalDetails extends Component {


    render() {
        const { item, close } = this.props
        return (
            <div className='P-details-modal'>
                <div className='P-details-modal-block'>
                    <div className='P-result-box G-flex G-justify-between'>
                        <span onClick={close} className='P-close-modal' style={{ backgroundImage: `url('${item.companyLogo}')` }} />
                        <div className='P-result-left-path G-flex'>
                            <div className='P-result-image' style={{ backgroundImage: `url('${getImage(item.companyLogo)}')` }} />
                            <div className='P-information'>
                                <h2>{item.companyName}</h2>
                                <h3>{item.title}</h3>
                                <span>{ item.description }</span>
                                <ul className="G-mt-2">
                                    <li><span style={{ backgroundImage: `url('${LocationImage}')` }} />{item.location}, {item.address}</li>
                                    <li><span style={{ backgroundImage: `url('${ClockImage}')` }} /> {item.employmentType}</li>
                                </ul>
                            </div>
                        </div>
                        <div className='P-result-right-path'>
                            <div className='P-bookmark-btn'>
                                <button onClick={(e) => this.props.mark(e, item.id, !item.bookmarked)}><span style={{ backgroundImage: `url('${BookmarkImage}')` }} />
                                    {item.bookmarked ? 'Bookmarked' : 'Bookmark'}
                                </button>
                            </div>
                            <div className='P-apply-btn'>
                                <button onClick={(e) => this.props.apply(e, item.id)}><span style={{ backgroundImage: `url('${BriefcaseImage}')` }} />
                                    {item.appliedForJob ? 'applied' : 'Apply for this job'}
                                </button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        );
    }
};

export default ModalDetails;
