class Content {
    id: number=0
    title: string="Accelerando Datenshi Tachi...";
    description: string="Lorem ipsum ";
    imgPath: string="../src/assets/poster.webp";
    likeCount: string="10k like";
    watchCount: string="10k watch";
    saveCount: string="10k save";
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
