<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
defineProps({
    title:"",
    isShow:Boolean,
    closeEvent:{
        type:Function
    },
    overlayCanClose:{
        type:Boolean,
        default:true
    }
})
defineEmits(["update:isShow"])
</script>
<template>
    <div v-if="isShow" class="theme_modal">
        <div class="modal_overlay" @click="overlayClick">
        </div>
        <div class="my_modal">
            <div v-if="title" class="modal_title d-flex justify-content-between">
                <h6 class="title">{{ title }}</h6>
            </div>
            <button @click="close"><FontAwesomeIcon icon="close" class="close-img"></FontAwesomeIcon></button>
            <div class="modal_body">
                <slot></slot>
            </div>
        </div>
    </div>
</template>
  
<script>
export default {
    methods:{
        close(){
            if(this.closeEvent){
            this.closeEvent();
            }
            else{
                this.$emit("update:isShow",false)
            }
        },
        overlayClick(){
            if(this.overlayCanClose){   
                this.close();
            }
        }
    }
}
</script>
<style scoped>
.theme_modal {
    position: fixed;
    top: 0;
    bottom: 0;
    left: 0;
    right: 0;
    display: flex;
    justify-content: center;
    align-items: center;
}

.modal_overlay {
    position: absolute;
    width: 100%;
    height: 100%;
    background-color: #000000da;
    z-index: -1;
}

.my_modal {
    background-color: var(--qua-color);
width: 500px;
padding: 15px 30px 30px;
border-radius: 20px;
position: relative;
}
.modal_title{
    border-bottom: 1px solid var(--pri-border-color);
}

h6 {
    font-weight: 500;
    font-size: 28px;
}

p {
    font-size: 16px;
    margin: 20px 0;
}

button {
font-size: 14px;
border-radius: 16px;
position: absolute;
right: 8px;
top: 0px;
padding: 5px;
}
</style>
  