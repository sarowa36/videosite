import {Source} from "./Source";
import {Guid} from "guid-typescript";
class Episode {
    id: string
    name: string="Accelerando Datenshi Tachi...";
    sourceList:Array<Source>=[]
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
export { Episode };
