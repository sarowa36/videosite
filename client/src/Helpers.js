export default {
    methods: {
        deepClone(val) {
            return JSON.parse(JSON.stringify(val));
        }
    }
}