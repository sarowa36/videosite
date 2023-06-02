<script setup>
import Textbox from '../../components/Textbox.vue';
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <form v-if="!Email.WaitingVerify && !Email.Verify" @submit="sendVerifyEmail"
                class="form col-md-9 col-lg-6 d-flex flex-column">
                <Textbox placeholder="Email Adresiniz" id="Email" type="email" />
                <button class="submit_btn mt-3 mb-3" type="submit">Emaili Doğrula</button>
            </form>
            <div v-if="Email.WaitingVerify" class="form col-md-9 col-lg-6 d-flex flex-column">
                <p>Lütfen Emailinize Gelen Bağlantıya Tıklayınız</p>
            </div>
            <form v-if="Email.Verify" @submit="(e) => { e.preventDefault() }"
                class="form col-md-9 col-lg-6 d-flex flex-column">
                <Textbox placeholder="Kullanıcı Adı" v-model="this.registerForm.email" />
                <Textbox placeholder="Şifrenizi Girin" id="Password" type="password" />
                <Textbox placeholder="Şifrenizi Tekrar Girin" id="PasswordConfirm" type="password" />
                <div class="mt-2">
                    <label for="state" style="width: 100%; padding-bottom:8px ;">Lütfen Favori Kategorilerinizi
                        Seçiniz</label>

                    <select class="js-example-basic-single" name="state" multiple="multiple">
                        <option value="AL">Horror</option>
                        <option value="WY">Comedy</option>
                    </select>
                </div>
                <button class="submit_btn mt-3 mb-3" type="submit">Kayıt Ol</button>
            </form>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            Email: {
                Verify: false,
                WaitingVerify: false
            },
            registerForm: {
                email: "",
                password: "",
                passwordConfirm: ""
            }
        }
    },
    methods: {
        sendVerifyEmail(e) {
            e.preventDefault()
            this.Email.WaitingVerify = true;
            setTimeout(() => {
                this.Email.Verify = true;
                this.Email.WaitingVerify = false;
            }, 5000)
        }
    },
    mounted() {
        $(document).ready(function () {
            $('.js-example-basic-single').select2({
                placeholder: "Lütfen Bir Tane Seçiniz",
                theme: "classic",
                dropdownAutoWidth: true,
                width: "100%"
            })
        });
    }
}
</script>