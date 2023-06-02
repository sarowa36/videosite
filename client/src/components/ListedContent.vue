<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
    <div class="mt-4 col-xxl-4 col-lg-6 listedcontent">
        <div class="title">
            Accelerando Datenshi Tachi...
        </div>
        <img src="@/assets/poster.webp" alt="">
        <div class="content_detail">
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempor accumsan metus, at viverra est tempus quis. Integer maximus dui ut velit ornare
            </p>
            <ul class="content_values">
                <li>
                    <span>10k</span>
                    <font-awesome-icon :icon="['far', 'bookmark']" />
                </li>
                <li>
                    <span>10k</span>
                    <font-awesome-icon :icon="['far', 'eye']" />
                </li>
                <li>
                    <span>10k</span>
                    <font-awesome-icon :icon="['far', 'thumbs-up']" />
                </li>
            </ul>
        </div>
    </div>
</template>
<script>
export default {

}
</script>
<style scoped>
.listedcontent{
    display: flex;
    flex-wrap: wrap;
}
.listedcontent > .title{
width: 100%;
background-color: var(--pri-cont-color);
padding: 5px;
border-radius: 3px 3px 0 0;
}
.listedcontent .content_detail{
    width: 60%;
    display: flex;
flex-direction: column;
background-color: var(--sec-cont-color);
justify-content: space-between;
}
.content_detail > p{
padding :10px;
margin: 0;
max-height: 55%;
overflow: hidden;
}
.listedcontent  img{
object-fit: cover;
aspect-ratio: 2/3;
max-width: 40%;
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
    .listedcontent  img{
        max-width: 50%;
    }   
    .listedcontent  .content_detail{
        width: 50%;
    }
}
</style>