class Filter {
    order: string = "recommend";
    keyword: string = ""
    category: Array<string> = [];
    release: string = "Hepsi";
    id: number = 0;
    constructor(p: object) {
        if (p) {
            Object.entries(p).forEach(item => {
                if(Array.isArray( this[item[0]]) && ["string","number"].includes( typeof p[item[0]] )){
                    this[item[0]].push(p[item[0]]);
                }
                else if (p[item[0]]) {
                    this[item[0]] = item[1];
                }
            })
        }
    }
    static CheckDefaultValue(propName: string, val: any): boolean {
        var def = new Filter({});
        if (Array.isArray(def[propName]) && Array.isArray(val))
            return JSON.stringify(def[propName])==JSON.stringify(val);
        else if(Array.isArray(def[propName])&& !Array.isArray(val))
            return false;
        return def[propName] == val;
    }
    static GetDefaultValue(propName:string){
        var def=new Filter({});
        return def[propName];
    }
}
export { Filter };