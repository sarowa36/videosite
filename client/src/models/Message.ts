class Message {
    id:number
    fromId:number
    toId:number
    text:""
    constructor(p: object) {
        if (p) {
            Object.entries(p).forEach(item => {
                if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }

    }
    
}
export { Message };
