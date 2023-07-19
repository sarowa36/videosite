<script setup>
import { RouterLink } from 'vue-router';
import EditLayout from '../../../components/EditLayout.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';

</script>
<template>
    <EditLayout user_role="admin">
        <RouterLink to="/Admin/Content/Create" class="btn btn_theme">Ekle</RouterLink>
        <table class="table table-striped table-dark w-100">
            <thead>
                <tr>
                    <td></td>
                    <td>Başlık</td>
                    <td></td>
                </tr>
            </thead>
            <tbody>
                <!-- <tr>
                    <td><img src="/src/assets/poster.webp" alt=""></td>
                    <td>Accelerando Datenshi Tachi...</td>
                    <td><button class="btn btn-primary me-1">
                            <FontAwesomeIcon icon="pencil" />
                        </button>
                        <button class="btn btn-danger">
                            <FontAwesomeIcon icon="trash" />
                        </button>
                    </td>
                </tr> -->
                <tr v-for="content in contentList" v-key="content.id">
                     <td><img :src="API_URL+content.imageLink" alt=""></td>
                    <td>{{ content.name }}</td>
                    <td><RouterLink :to="'/Admin/Content/Update/'+content.id" class="btn btn-primary me-1">
                            <FontAwesomeIcon icon="pencil" />
                        </RouterLink>
                        <button class="btn btn-danger">
                            <FontAwesomeIcon icon="trash" />
                        </button> 
                    </td>
                </tr>
            </tbody>
        </table>
    </EditLayout>
</template>
<style scoped>
table img {
    height: 150px;
}

.btn_theme {
    float: right;
    color: white;
    margin-bottom: 15px;
}
</style>
<script>
export default {
    data() {
        return {
            contentList: []
        }
    },
    methods:{
        async fetchData(){
            this.contentList=await $.ajax(this.API_URL+"api/Content/Getlist");
        }
    },
    async mounted() {
        this.fetchData();
    }
}
</script>