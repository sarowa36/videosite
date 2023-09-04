<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Textbox from '../../components/Textbox.vue';
import axios from 'axios';
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <div class="form col-md-9 col-lg-6 d-flex flex-column">
                <FontAwesomeIcon icon="user" class="formusericon"></FontAwesomeIcon>
                <span class="text-danger" v-for="e in errors.modelOnly">{{ e }}</span>
                <Textbox placeholder="Kullanıcı Adı" v-model="userName" />
                <span class="text-danger" v-for="e in errors.userName">{{ e }}</span>
                <Textbox type="password" placeholder="Şifre" v-model="password" />
                <span class="text-danger" v-for="e in errors.password">{{ e }}</span>
                <RouterLink to="/Identity/ForgotPassword" class="forgot_password">Şifremi Unuttum</RouterLink>
                <div class="createaccount">Hesabınız Yok Mu <RouterLink to="/Identity/Register">Bir Tane Oluşturun</RouterLink>
                </div>
                <button class="submit_btn mt-3 mb-3" type="submit" @click="sendData">Giriş Yap</button>
            </div>
        </div>
    </div>
</template>
<style scoped>
.formusericon {
    font-size: 110px;
}

.forgot_password {
    margin-top: 5px;
    width: fit-content;
}

@media (max-width:768px) {
    .formusericon {
        font-size: 60px;
    }
}
</style>
<script>
export default {
    data() {
        return {
            userName: "",
            password: "",
            errors:{}
        }
    },
    methods:{
        async sendData(){
            var d={userName:this.userName, password:this.password};
            const {data, status} =await axios.postForm("identity/login",d,{
                withCredentials:true
            });
            if(status==200){
                location.pathname="/";
            }
            else{
                this.errors=data;
            }
        }
    }
}
</script>