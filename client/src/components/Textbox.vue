<template>
    <div class="form_group">
        <span v-if="!this.error.isValid"></span>
        <input :type="_type" class="input_text" :placeholder="placeholder" @blur="checkVal" />
        <label :for="this.id" class="input_label">{{placeholder}}</label>
    </div>
</template>
<script>
export default {
    data(){
        return{
        _type:"text",
        error:{
            isValid:true,
            list:[]
        }
        };
    },
    methods:{
        checkVal(e){
           const node=e.target;
           if(node.value && !node.classList.contains("with_value")){
            node.classList.add("with_value");
           }
           else if(!node.value && node.classList.contains("with_value"))
           {
            node.classList.remove("with_value");
           }
        }
    },
    mounted() {
        if(this.type){
            this._type=this.type
        }
    },
    props: ["compare","model","type", "placeholder"]
}
</script>
<style scoped>
.input_label {
    width: fit-content;
    transform: translateY(100%);
    cursor: text;
    color: var(--pri-label-color);
}

.input_text:focus, .input_text.with_value {
    border-bottom-color: var(--sec-cont-color);
}

.input_text:focus+.input_label, .input_text.with_value + .input_label {
    transform: scale(85%);
}

.input_text {
    background-color: transparent;
    border: 0 var(--pri-border-color) solid;
    border-bottom: 1px solid var(--pri-border-color);
    color: var(--pri-textbox-color);
}

.form_group {
    display: flex;
    flex-direction: column-reverse;
}

input.input_text::placeholder {
    color: transparent;
}

input.input_text:focus,
input.input_text:focus-visible {
    outline: none;
}</style>