<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import Textbox from '../../components/Textbox.vue';
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <form class="col-6 d-flex flex-column">
                <FontAwesomeIcon icon="user" class="formusericon"></FontAwesomeIcon>
                <Textbox placeholder="Kullanıcı Adı" id="UserId" />
                <Textbox type="password" placeholder="Şifre" id="Password" />
                <RouterLink to="/User/ForgotPassword">Şifremi Unuttum</RouterLink>
                <button class="submit_btn mt-3 mb-3" type="submit">Giriş Yap</button>
            </form>
        </div>
    </div>
</template>
<style scoped>

.form-row{
height: 70vh;
}
form{
    background-color: var(--pri-color);
    border-radius: 5px;
    padding:15px;
}

.formusericon{
font-size: 110px;
}
.submit_btn{
    width: fit-content;
    align-self: center;
    background-color: var(--pri-btn-color);
    padding: 3px 5px;
    border: 1px solid var(--pri-border-color);
    border-radius: 5px;
}
</style>