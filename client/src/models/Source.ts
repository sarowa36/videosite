import {Guid} from "guid-typescript";
class Source {
    id: string
    title: string="Accelerando Datenshi Tachi...";
    description: string="Lorem ipsum ";
    imgPath: string="../src/assets/poster.webp";
    likeCount: string="10k like";
    watchCount: string="10k watch";
    saveCount: string="10k save";
    episode_id:string
    constructor(p: object) {
        if (p) {
            Object.entries(p).forEach(item => {
                if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }
        if(!Object.keys(p).includes("id") ||p==null){
            this.id = Guid.create().toString();
        }
    }
}
export { Source };
