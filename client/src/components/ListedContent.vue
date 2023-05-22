<script setup>
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>
<template>
    <div class="col-3 listedcontent">
        <div class="title">
            Accelerando Datenshi Tachi...
        </div>
        <img src="@/assets/e.webp" alt="">
        <div class="content_detail">
            <p>
                Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam tempor accumsan metus, at viverra est tempus quis. Integer maximus dui ut velit ornare
            </p>
            <ul class="content_values">
                <li>
                    <span>10k</span>
                    <FontAwesomeIcon icon="anchor"></FontAwesomeIcon>
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
}
.content_detail > p{
padding :10px;
margin: 0;
max-height: 55%;
overflow: hidden;
}
.listedcontent  img{
    width: 40%;
    height: 100%;
    object-fit: cover;
    aspect-ratio: 2/3;
}
</style>