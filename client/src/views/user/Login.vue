<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Textbox from '../../components/Textbox.vue';
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <form class="form col-md-9 col-lg-6 d-flex flex-column">
                <FontAwesomeIcon icon="user" class="formusericon"></FontAwesomeIcon>
                <Textbox placeholder="Kullanıcı Adı" id="UserId" />
                <Textbox type="password" placeholder="Şifre" id="Password" />
                <RouterLink to="/User/ForgotPassword" class="forgot_password">Şifremi Unuttum</RouterLink>
                <div class="createaccount">Hesabınız Yok Mu <RouterLink to="/User/Register">Bir Tane Oluşturun</RouterLink></div>
                <button class="submit_btn mt-3 mb-3" type="submit">Giriş Yap</button>
            </form>
        </div>
    </div>
</template>
<style scoped>
.formusericon{
font-size: 110px;
}
.forgot_password{
    margin-top: 5px;
    width: fit-content;
}
@media (max-width:768px) {
    .formusericon{
        font-size: 60px;
    }
}
</style>