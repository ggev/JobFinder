import React from 'react';

import ClickOutside from '../click-outside/Click-outside';

import './style.css';

class Modal extends React.Component {



    render() {
        const { isOpen, close, changeClass } = this.props;
        return isOpen ? 
            <div className={`E-modal-wrap`} >
                <ClickOutside onClickOutside={close}>
                    <div className={`E-modal-content ${changeClass} `}>
                        {this.props.children}
                    </div>
                </ClickOutside>
            </div>
        : <div />;
    }
}

export default Modal;
