class Comment {
    text:String
    isOverflow:boolean
    imageLink:String
    userName:String
    constructor(p: object) {
        if (p) {
            Object.entries(p).forEach(item => {
                if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }
        if(this.text==null){
        this.isOverflow=false;    
        }
        else{
            this.isOverflow=this.text.length > 350;
        }
    }
    
}
export { Comment };
