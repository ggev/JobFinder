import Connection from '../service/index';

const controllerName = 'category';

class CategoryController {

    static getList = () => {
        const result = Connection.GET(controllerName, 'getList',);
        return result;
    };

}
export default CategoryController;