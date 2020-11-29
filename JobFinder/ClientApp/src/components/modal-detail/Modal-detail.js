import React, { Component } from 'react';
import './style.css';
import LocationImage from "../../assets/images/pin.png";
import ClockImage from "../../assets/images/clock.png";
import BookmarkImage from "../../assets/images/bookmark.png";
import CloseImage from "../../assets/images/cancel.png";

import BriefcaseImage from "../../assets/images/briefcase.png";

class ModalDetails extends Component {


    render() {
        const { item, close } = this.props
        return (
            <div className='P-details-modal'>
                <div className='P-details-modal-block'>
                    <div className='P-result-box G-flex G-align-center G-justify-between'>
                        <span onClick={close} className='P-close-modal' style={{ backgroundImage: `url('${CloseImage}')` }} />
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
                </div>
            </div>
        );
    }
};

export default ModalDetails;
