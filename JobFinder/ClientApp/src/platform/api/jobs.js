import Connection from '../service/index';

const controllerName = 'job';

class JobController {

    static getList = (data) => {
        const result = Connection.POST(controllerName, 'getList', data);
        return result;
    };
    static bookmark = (data) => {
        const result = Connection.PUT(controllerName, 'bookmark', {} , data);
        return result;
    } 
    static apply = (data) => {
        const result = Connection.PUT(controllerName, 'apply', null, data);
        return result;
    } 
    static detail = (id) => {
        const result = Connection.GET(controllerName, `${id}`);
        return result;
    } 
}
export default JobController;