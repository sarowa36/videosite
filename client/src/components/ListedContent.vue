<script setup >
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
import {Content} from "../models/Content";
import {RouterLink} from "vue-router"
defineProps({
    content:{
        type:Content,
    }
})
</script>
<template>
    <div class="mt-4 col-xxl-4 col-lg-6 listedcontent">
        <router-link :to="'/Content/'+content.id" class="title">
            {{content.name}}
        </router-link>
        <router-link class="content_image" :to="'/Content/'+content.id">
        <img :src="API_URL+content.imageLink" alt="">
        </router-link>
        <div class="content_detail">
            <p>
               {{ content.description }}
            </p>
            <ul class="content_values">
                <li>
                    <span>{{ content.saveCount }}</span>
                    <font-awesome-icon :icon="['far', 'comments']" />
                </li>
                <li>
                    <span>{{ content.watchCount }}</span>
                    <font-awesome-icon :icon="['far', 'eye']" />
                </li>
                <li>
                    <span>{{ content.likeCount }}</span>
                    <font-awesome-icon :icon="['far', 'thumbs-up']" />
                </li>
            </ul>
        </div>
    </div>
</template>
<style scoped>
.listedcontent{
    display: flex;
    flex-wrap: wrap;
    height: 350px;
}
.listedcontent > .title{
width: 100%;
background-color: var(--pri-cont-color);
padding: 5px;
border-radius: 3px 3px 0 0;
color: var(--pri-t-color);
}
.listedcontent .content_detail{
    width: 60%;
    display: flex;
flex-direction: column;
background-color: var(--sec-cont-color);
justify-content: space-between;
max-height: calc(100% - 34px);
}
.content_detail > p{
padding :10px;
margin: 0;
max-height: 55%;
overflow: hidden;
}
.listedcontent  .content_image{
width: 40%;
max-height: calc(100% - 34px);
}
.listedcontent  .content_image img{
object-fit: cover;
aspect-ratio: 2/3;
height: 100%;
width: 100%;
}
.content_values{
    padding: 0 10px 10px;
    margin: 0;
}
.content_values > li{
    display: flex;
    justify-content: space-between;
    align-items: center;
    font-size: 20px;
}
.content_values > li:nth-child(1){
    margin-top: 7px;
}
.content_values > li svg{
    width: 15%;
}
@media (max-width:576px) {
    .listedcontent .content_image{
        width: 50%;
    }   
    .listedcontent  .content_detail{
        width: 50%;
    }
}
</style>