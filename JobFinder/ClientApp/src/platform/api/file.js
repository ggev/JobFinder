import Connection from '../service/index';

const controllerName = 'file';

class FileController {

    static setImage = (name) => {
        const result = Connection.POST(controllerName, `${0}/${name}`);
        return result;
    };
}
export default FileController;