
const getImage = (name) => {
    const path = window.location.href;
    return `${path}api/file/0/${name}`
}
export default getImage;