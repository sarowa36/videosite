import {Episode} from "./Episode";
class Content {
    id: number=0
    name: string="";
    description: string="";
    imageLink: string="src/assets/poster.webp";
    likeCount: string="";
    watchCount: string="";
    saveCount: string="";
    episodeList:Array<Episode>=[];
    categories:[]
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
export { Content };
