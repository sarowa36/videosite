<script setup>
import EditLayout from '../../components/EditLayout.vue';
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
<EditLayout user_role="admin">
    <table class="table table-striped table-dark w-100">
        <thead>
            <tr>
                <td></td>
                <td>Başlık</td>
                <td>İzlenme Sayısı</td>
                <td></td>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td><img src="/src/assets/poster.webp" alt=""></td>
                <td>Accelerando Datenshi Tachi...</td>
                <td>100.000</td>
                <td><button class="btn btn-primary me-1"><FontAwesomeIcon icon="pencil" /></button>
                    <button class="btn btn-danger"><FontAwesomeIcon icon="trash" /></button></td>
            </tr>
        </tbody>
    </table>
</EditLayout>
</template>
<style>
table  img{
    height: 150px;
}
</style>