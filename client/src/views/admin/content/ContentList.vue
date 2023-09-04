<script setup>
import { RouterLink } from 'vue-router';
import EditLayout from '../../../components/EditLayout.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import YesOrNoDialogModal from "../../../components/YesOrNoDialogModal.vue";
import axios from 'axios';
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
                        <button class="btn btn-danger" @click="modalShow(content.id)">
                            <FontAwesomeIcon icon="trash" />
                        </button> 
                    </td>
                </tr>
            </tbody>
        </table>
    </EditLayout>
    <YesOrNoDialogModal v-model:is-show="isModalShowing" :okEvent="deleteContent" />
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
            contentList: [],
            isModalShowing:false,
            removeSelectedId:0
        }
    },
    methods:{
        async fetchData(){
            var req=await axios.get("Content/Getlist");
            if(req.status==200){
            this.contentList=req.data;
        }
        },
        modalShow(id){
            this.isModalShowing=true;
            this.removeSelectedId=id;
        },
        async deleteContent(){
            var result=await axios.get("content/delete/"+this.removeSelectedId);
            if(result.status==200){
                this.isModalShowing=false;
                this.removeSelectedId=0;
                this.contentList=this.contentList.filter(x=>x.id!=this.removeSelectedId);
            }
        }
    },
    async mounted() {
        this.fetchData();
    }
}
</script>