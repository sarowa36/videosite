<script setup>
import Textbox from '../../components/Textbox.vue';
import {HubConnectionBuilder, LogLevel} from "@microsoft/signalr"
import Select2 from "../../components/Select2.vue"
import axios from "axios";
import router from "../../router/index";
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center mt-4 mb-4">
            <div v-if="position==-1" class="form col-md-9 col-lg-6 d-flex flex-column">
                <p>Geçersiz Email Adresi</p>
            </div> 
            <form v-if="position==0" @submit="sendVerifyEmail"
                class="form col-md-9 col-lg-6 d-flex flex-column">
                <Textbox placeholder="Email Adresiniz" v-model="registerForm.email" />
                <button class="submit_btn mt-3 mb-3" type="submit">Emaili Doğrula</button>
            </form>
             <div v-if="position==1" class="form col-md-9 col-lg-6 d-flex flex-column">
                <p>Lütfen Emailinize Gelen Bağlantıya Tıklayınız</p>
            </div> 
            <div v-if="position==2" 
                class="form col-md-9 col-lg-6 d-flex flex-column">
                <Textbox placeholder="Kullanıcı Adı" v-model="registerForm.userName"  />
                <span class="text-danger" v-for="e in errors.userName">{{ e }}</span>
                <Textbox placeholder="Şifrenizi Girin" v-model="registerForm.password" type="password" />
                <span class="text-danger" v-for="e in errors.password">{{ e }}</span>
                <Textbox placeholder="Şifrenizi Tekrar Girin" v-model="registerForm.password2" type="password" />
                <span class="text-danger" v-for="e in errors.password2">{{ e }}</span>
                <div class="mt-2">
                    <label for="state" style="width: 100%; padding-bottom:8px ;">Lütfen Favori Kategorilerinizi
                        Seçiniz</label>
                    <select2 class="js-example-basic-single" multiple="multiple" v-model="registerForm.categories">
                        <option v-for="cat in allCategories" :value="cat.id">{{ cat.name }}</option>
                    </select2>
                </div>
                <img v-if="base64img!=null" class="register_preview_profileimg mt-3" :src="base64img" alt="">
                <Textbox type="file" accept="image/*" id="profileimg" placeholder="Profil Fotoğrafı seçiniz" class="align-items-center mt-3" v-model="base64img" />
                <span class="text-danger" v-for="e in errors.profileImage">{{ e }}</span>
                <button class="submit_btn mt-3 mb-3" type="submit" @click="sendRequest">Kayıt Ol</button>
            </div>
        </div>
    </div>
</template>
<style>
.register_preview_profileimg{
    height: 180px;
    object-fit: contain;
}
</style>
<script>
export default {
    data() {
        return {
            registerForm: {
                email: "",
                userName:"",
                password: "",
                password2: "",
                categories:[]
            },
            errors:{},
            position:0,
            allCategories:[],
            base64img:null,
            connectionId:null,
            connection:null
        }
    },
    methods: {
        async sendVerifyEmail(e) {
            e.preventDefault()
            if(this.registerForm.email!=""){
             this.position=1;
             this.connection.invoke("SendVerifyEmailRequest",this.registerForm.email);
            }
        },
        async sendRequest(){
            var data=this.registerForm;
            data.profileImage=document.querySelector("#profileimg").files[0];
            var result=(await axios.postForm("identity/register",data));
            if(result.status==200){
                location.pathname="/";
            }
            else{
                this.errors=result.data;
            }
        }
    },
    async created(){
        var req=(await axios.get("category/getlist"));
        if(req.status==200){
        this.allCategories=req.data;
    }
        const connection=new HubConnectionBuilder().withUrl(this.API_URL+"hub/verifyEmail").configureLogging(LogLevel.Information).build();
        await connection.start();
        this.connection=connection;
        this.connectionId=this.connectionId;
        this.connection.on("emailStatus",(val)=>{
            if(!val){
                this.position=-1;
            }
        })
        this.connection.on("emailAccepted",()=>{
            this.position=2;
        })
    },
   watch:{
   }
}
</script>