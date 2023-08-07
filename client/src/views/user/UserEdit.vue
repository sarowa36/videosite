<script setup>
import EditLayout from '../../components/EditLayout.vue';
import Textbox from "../../components/Textbox.vue"
import Select2 from "../../components/Select2.vue";
import axios from 'axios';
</script>
<template>
    <EditLayout>
        <div class="row justify-content-end">
            <div class="col-6">
                <label>Favori Kategoriler</label>
        <select2 class="js-example-basic-single" multiple="multiple" v-model="editForm.categories">
                        <option v-for="cat in allCategories" :value="cat.id">{{ cat.name }}</option>
         </select2></div>
         <div class="col-6">
            <label>Profil Fotoğrafı</label><br>
        <img class="register_preview_profileimg mt-3" :src="base64img ?? editForm.imageLink" alt="">
        <Textbox type="file" accept="image/*" id="profileimg" placeholder="Profil Fotoğrafı seçiniz"
            class="align-items-center mt-3" v-model="base64img" />
        </div>
        <button class="col-2 btn_theme" @click="sendRequest">Kaydet</button>
    </div>
    </EditLayout>
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
            base64img: null,
            allCategories:[],
            editForm:{
                categories:null,
                imageLink:""
            }
        }
    },
    methods:{
        async sendRequest(){
            var result=(await axios.postForm("identity/profileEdit",this.editForm))
        }
    },
    async created(){
        this.allCategories=(await axios.get("category/getlist")).data;
        this.editForm=(await axios.get("identity/ProfileEdit")).data;
    }
}
</script>