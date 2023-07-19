<script setup >
import { HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
</script>
<template>
    <div class="container">
        <div class="row align-items-center form-row justify-content-center">
            <h1>email başarıyla doğrulanmıştır lütfen önceki sekmeye geri dönün</h1>
            </div>
        </div>
</template>
<script>
export default {
    data() {
        return {}
    },
    async created() {
        const connection=new HubConnectionBuilder().withUrl(this.API_URL+"hub/verifyEmail").configureLogging(LogLevel.Information).build();
        await connection.start();
        connection.invoke("verifyEmail",this.$route.params.id);
    }
}
</script>