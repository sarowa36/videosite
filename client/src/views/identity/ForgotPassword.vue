<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Textbox from '../../components/Textbox.vue';
import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr"
import axios from 'axios';
import router from '../../router';
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <p v-if="position==-2" class="form col-md-9 col-lg-6">Önceki Sayfaya Geri Dönün Lütfen</p>
            <form v-else-if="position==0" @submit="sendEmailRequest"
                class="form col-md-9 col-lg-6 d-flex flex-column">
                <Textbox placeholder="Kullanıcı Adı" v-model="newPasswordForm.userName" />
                <button class="submit_btn mt-3 mb-3" type="submit">Şifreyi Yenile</button>
            </form>

            <p v-else-if="position==1" class="form col-md-9 col-lg-6">Lütfen Emailinizi Kontrol Ediniz</p>
            <div v-else-if="position==2" class="form col-md-9 col-lg-6 d-flex flex-column">
                <span class="text-danger" v-for="e in errors.modelOnly">{{ e }}</span>
                <Textbox type="password" placeholder="Yeni Şifrenizi Giriniz" v-model="newPasswordForm.password"></Textbox>
                <span class="text-danger" v-for="e in errors.password">{{ e }}</span>
                <Textbox type="password" placeholder="Yeni Şifrenizi Tekrar Giriniz" v-model="newPasswordForm.passwordConfirm"></Textbox>
                <span class="text-danger" v-for="e in errors.passwordConfirm">{{ e }}</span>
                <button class="submit_btn mt-3 mb-3" type="submit" @click="refreshPassword">Şifreyi Yenile</button>
            </div>
            <p v-else class="form col-md-9 col-lg-6">Böyle Bir Kullanıcı Yok</p>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
           
            position:0,
            connection:null,
            connectionId:null,
            errors:{},
            newPasswordForm:{
                userName:"",
                password:"",
                passwordConfirm:"",
                token:""
            }
        }
    },
    methods: {
        async sendEmailRequest(e) {
            e.preventDefault();
            this.connection.invoke("forgotPassword",this.newPasswordForm.userName)
        },
        async refreshPassword(){
            var response=(await axios.postForm("identity/refreshpassword",this.newPasswordForm)).data
            if(response.succeeded){
                router.push("/identity/login")
            }
            else{
                this.errors=response;
            }
        }
    },
    async created() {
        if(this.$route.params.token){
            axios.postForm("identity/forgotpasswordToken",{token:this.$route.params.token});
            this.position=-2;
        }
        const connection = new HubConnectionBuilder().withUrl(this.API_URL + "hub/verifyEmail").configureLogging(LogLevel.Information).build();
        await connection.start();
        this.connection = connection;
        this.connectionId = this.connectionId;
        this.connection.on("emailExistence", (val) => {
            if(val){
                this.position++;
            }
            else{
                this.position--;
            }
        })
        this.connection.on("emailVerified",(token)=>{
            this.position++;
            this.newPasswordForm.token=token;
        })
    }
}
</script>