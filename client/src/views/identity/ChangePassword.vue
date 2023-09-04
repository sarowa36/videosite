<script setup>
import axios from "axios";
import EditLayout from "../../components/EditLayout.vue";
import Textbox from "../../components/Textbox.vue";
import router from "../../router";
</script>
<template>
    <EditLayout>
        <div class="row justify-content-end">
            <div class="col-12 text-center d-flex flex-column">
                <span class="text-danger" v-for="e in errors.modelOnly">{{ e }}</span>
            </div>
            <div class="col-6">
                <Textbox v-model="oldPassword" type="password" placeholder="Eski Şifre"></Textbox>
                <span class="text-danger" v-for="e in errors.oldPassword">{{ e }}</span>
            </div>
            <div class="col-6">
                <Textbox v-model="newPassword" type="password" placeholder="Yeni Şifre"></Textbox>
                <span class="text-danger" v-for="e in errors.newPassword">{{ e }}</span>
                <Textbox v-model="newPasswordConfirm" type="password" placeholder="Yeni Şifre Tekrar"></Textbox>
                <span class="text-danger" v-for="e in errors.newPasswordConfirm">{{ e }}</span>
            </div>
            <button class="btn_theme col-2 mt-3" @click="sendRequest">Kaydet</button>
        </div>
    </EditLayout>
</template>
<script>
export default {
    data() {
        return {
            oldPassword: "",
            newPassword: "",
            newPasswordConfirm: "",
            errors:{}
        }
    },
    methods:{
        async sendRequest(){
            var {data,status}=(await axios.postForm("identity/changepassword",this.$data));
            if(status==200){
                router.go(-1)
            }
            else{
                this.errors=data;
            }
        }
    }
}
</script>