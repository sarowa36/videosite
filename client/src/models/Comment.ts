class Comment {
    id: number=0
    img:String="/src/assets/poster.webp"
    text:String
    isOverflow:boolean

    constructor(p: object) {
        if (p) {
            Object.entries(p).forEach(item => {
                if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }
        console.log(this.text)
        this.isOverflow=this.text.length > 350;
    }
}
export { Comment };
