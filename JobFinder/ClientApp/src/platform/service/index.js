
class Connection {

    const getDeviceId = (): Promise<string> => {
        return new Promise(async res => {
            await fingerprint2.get((components) => {
                const VALUES = components.filter(x => x.key !== 'adBlock').map(item => item.value);
                const DEVICE_ID = fingerprint2.x64hash128(VALUES.join(''), 31);
                res(DEVICE_ID);
            });
        });
    }


    static queryFromObject = obj => {
        const str = [];

        for (const query in obj) {
            if (obj.hasOwnProperty(query) && obj[query]) {
                const string = encodeURIComponent(query) + "=" + encodeURIComponent(obj[query]);
                str.push(string);
            }
        }

        return str.join("&");
    }

    static createHeaders = () => {
        const HEADERS = new Headers();
        const guestId = await Connection.getDeviceId();
        HEADERS.append('userId', guestId);
        HEADERS.append('Content-Type', 'application/json');
        return HEADERS;
    }
    static responseRestructure = response => {
        return response ? response.json() : {};
    }
    static POST = async (controllerName, actionName, body) => {
        const HEADERS = Connection.createHeaders();
        window.pendingRequest = true;
        const response = await fetch(`api/${controllerName}/${actionName}`, {
            body: JSON.stringify(body),
            method: 'POST',
            headers: HEADERS,
            credentials: 'include',
        });

        window.pendingRequest = false;
        return Connection.responseRestructure(response);
    }

    static GET = async (controllerName, actionName, queryConfig) => {
        const onlyQuery = !actionName && queryConfig;
        const HEADERS = Connection.createHeaders();
        window.pendingRequest = true;
        const response = await fetch(`api/${controllerName}${!onlyQuery ? '/' : ''}${actionName}${queryConfig ? `?${Connection.queryFromObject(queryConfig)}` : ''}`, {
            method: 'GET',
            headers: HEADERS,
            credentials: 'include',
        });

        window.pendingRequest = false;
        return Connection.responseRestructure(response);
    }
}

export default Connection;