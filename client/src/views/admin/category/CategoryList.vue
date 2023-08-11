<script setup>
import { RouterLink } from 'vue-router';
import EditLayout from '../../../components/EditLayout.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import YesOrNoDialogModal from '../../../components/YesOrNoDialogModal.vue';
import axios from 'axios';
</script>
<template>
    <EditLayout user_role="admin">
        <RouterLink to="/Admin/Category/Create" class="btn btn_theme">Ekle</RouterLink>
        <table class="table table-striped table-dark w-100">
            <thead>
                <tr>
                    <td>Başlık</td>
                    <td></td>
                </tr>
            </thead>
            <tbody>
                <tr v-for="content in contentList" v-key="content.id">
                    <td>{{ content.name }}</td>
                    <td><RouterLink :to="'/Admin/Category/Update/'+content.id" class="btn btn-primary me-1">
                            <FontAwesomeIcon icon="pencil" />
                        </RouterLink>
                        <button class="btn btn-danger" @click="openModal(content.id)">
                            <FontAwesomeIcon icon="trash" />
                        </button> 
                    </td>
                </tr>
            </tbody>
        </table>
    </EditLayout>
    <YesOrNoDialogModal v-model:is-show="isShowModal" :ok-event="okEvent" />
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
            isShowModal:false,
            currentDeletingId:0
        }
    },
    methods:{
        async fetchData(){
            this.contentList=(await axios.get("Category/Getlist")).data;
        },
        openModal(val){
            this.isShowModal=true;
            this.currentDeletingId=val;
        },
        async okEvent(){
            var result=await axios.get("category/delete/"+this.currentDeletingId);
            if(result.status==200){
                this.contentList=this.contentList.filter(x=>x.id!=this.currentDeletingId);
                this.isShowModal=false;
                this.currentDeletingId=0;
            }
        }
    },
    async mounted() {
        this.fetchData();
    }
}
</script>