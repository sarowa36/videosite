<script setup>
import DialogModal from "./DialogModal.vue"

defineProps({
    title: {
        type:String,
        default:"Uyarı"
    },
    text:{
        type:String,
        default:"Silmek İstediğinize Emin Misiniz?"
    },
    isShow: Boolean,
    closeEvent: {
        type: Function
    },
    okEvent: {
        type: Function
    }
})
defineEmits(["update:isShow"])
</script>
<template>
    <DialogModal :overlayCanClose="true" :title="title" v-model:isShow="value" :closeEvent="close">
        <div>
            {{ text }}
        </div>
        <div class="d-flex float-end">
            <button class="btn btn-secondary me-2" @click="close">Hayır</button>
            <button class="btn btn-danger" @click="ok">Evet</button>
        </div>
    </DialogModal>
</template>
<script>
export default {
    methods: {
        close(){
            if(this.closeEvent){
                this.closeEvent();
            }
            else{
                this.$emit("update:isShow",false)
            }
        },
        ok(){
            if(this.okEvent){
                this.okEvent();
            }
            else{
                this.$emit("update:isShow",false)
            }
        }
    },
    computed:{
        value:{
            get(){
                return this.isShow;
            },
            set(val){
                this.$emit("update:isShow",val)
            }
        }
    }
}
</script>