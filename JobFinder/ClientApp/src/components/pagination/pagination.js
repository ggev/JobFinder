import React, { Component } from 'react';
import './index.css';

class Pagination extends Component {
    state = {
        selectedPage: 1
    }
    Pages = () => {
        const { pageCount, callback } = this.props;
        const { selectedPage } = this.state;
        const arr = [];
        for (let i = 1; i <= pageCount; i++) {
            arr.push(i);
        }
        return <div className="P-pagination">
            {arr.map((item, index) => <div
                key={index}
                onClick={() => {
                    this.setState({ selectedPage: item });
                    callback(item);
                }}
                className={`P-pagination-item ${selectedPage === item ? 'P-pagination-selected-item' : ''}`}>{item}</div>)}
        </div>
    }
    render() {
        return <this.Pages />
    }
};

export default Pagination;
 