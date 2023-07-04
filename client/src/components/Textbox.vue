<script setup>
import { computed } from 'vue';

defineProps({
    type:{
        type:String,
        default:"textbox"
    },
    placeholder:"",
    modelValue:"",
    accept:"",
    height:"",
    id:""
})
defineEmits(["update:modelValue"])
</script>
<template>
    <div class="form_group">
        <span v-if="!this.error.isValid"></span>
        <input ref="textbox" v-if="type!='textarea'" :id="id" :type="type" class="myinput" :placeholder="placeholder" @blur="checkVal" v-model="value" :accept="accept" />
        <textarea ref="textbox" v-else class="myinput" :placeholder="placeholder" @blur="checkVal" v-model="value" :style="'height:'+height"></textarea>
        <label :for="id" class="input_label">{{placeholder}}</label>
    </div>
</template>
<script>
export default {
    data(){
        return{
        error:{
            isValid:true,
            list:[]
        }
        };
    },
    methods:{
        checkVal(e){
           const node=this.$refs.textbox;
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
     const node=this.$refs.textbox;
        if(this.modelValue){
            node.value=this.modelValue;
            node.classList.add("with_value")
        }
    },
    computed:{
      value:{
        get(){
            return this.modelValue;
        },
        set(val){
            this.$emit("update:modelValue", val)
        }
      }  
    }
}
</script>
<style scoped>
.input_label {
    width: fit-content;
    transform: translateY(100%);
    cursor: text;
    color: var(--pri-label-color);
}

.myinput:focus, .myinput.with_value {
    border-bottom-color: var(--sec-cont-color);
}

.myinput:focus+.input_label, .myinput.with_value + .input_label {
    transform: scale(85%);
}

.myinput {
    background-color: transparent;
    border: 0 var(--pri-border-color) solid;
    border-bottom: 1px solid var(--pri-border-color);
    color: var(--pri-textbox-color);
    outline: none;
}

.form_group {
    display: flex;
    flex-direction: column-reverse;
}

input.myinput::placeholder, textarea.myinput::placeholder {
    color: transparent;
}

input.myinput:focus,
input.myinput:focus-visible {
    outline: none;
}
input.myinput[type="file"]{
    display:none;
}
input.myinput[type="file"]+label{
    transform: initial;
    padding: 10px;
border: 1px dashed var(--pri-border-color);
cursor: pointer;
}
input.myinput[type="file"]+label:hover{
    background-color: var(--sec-btn-active-color);
}
</style>