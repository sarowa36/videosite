import {Guid} from "guid-typescript";
class Source {
    id: string
    name: string="Accelerando Datenshi Tachi...";
    imgPath: string="../src/assets/poster.webp";
    iframeCode:""
    episode_id:string
    constructor(p: object) {
        if (p) {
            Object.entries(p).forEach(item => {
                if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }
        if(p==null || !Object.keys(p).includes("id") ){
            this.id = Guid.create().toString();
        }
    }
}
export { Source };
