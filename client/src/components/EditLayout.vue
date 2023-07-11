<script setup>
import { FontAwesomeIcon } from "@fortawesome/vue-fontawesome";
import {RouterLink} from "vue-router"
defineProps({
    user_role: String
})
</script>
<template>
    <div  :class='(user_role=="admin" ? "container-fluid" :"container")+" mt-5 mb-5 content"'>
        <div class="row">
            <div :class='(user_role=="admin" ? "col-2": "col-3")+" tab"'>
                <img src="/src/assets/profile.png" alt="">
                <ul>
                   <!-- <li v-if="user_role == 'admin'" class="with_sub"><a href="#">
                            <FontAwesomeIcon icon="film" />
                            İçerikler
                            <FontAwesomeIcon icon="arrow-down" />
                        </a>
                        <ul>
                            <li><RouterLink to="/Admin/Create/Content">Create</RouterLink></li>
                            <li><RouterLink to="/Admin/List/Content">List</RouterLink></li>
                        </ul>
                    </li>-->
                    <li v-if="user_role == 'admin'"><RouterLink to="/Admin/Content/List">
                            <FontAwesomeIcon icon="film" />
                            İçerikler
                        </RouterLink>
                    </li>
                    <li v-if="user_role == 'admin'" class="with_sub"><RouterLink to="/Admin/Content/List">
                            <FontAwesomeIcon icon="folder" />
                            Kategoriler
                            <FontAwesomeIcon icon="arrow-down" />
                        </RouterLink>
                        <ul>
                            <li><RouterLink to="/Admin/Create/Category">Create</RouterLink></li>
                            <li><RouterLink to="/Admin/List/Category">List</RouterLink></li>
                        </ul>
                    </li>
                    <li><a href="#">
                            <FontAwesomeIcon icon="bell" />Bildirimler
                        </a></li>
                    <li><a href="#">
                            <FontAwesomeIcon icon="user" />Profilini Düzenle
                        </a></li>
                    <li><a href="#">
                            <FontAwesomeIcon icon="key" />Şifre Değiştir
                        </a></li>
                    <li><a href="#">
                            <FontAwesomeIcon icon="arrow-right-from-bracket" />Çıkış Yap
                        </a></li>
                </ul>
            </div>
            <div :class='user_role=="admin" ? "col-10" :"col-9"'>
                <slot></slot>
            </div>
        </div>
    </div>
</template>
<style>
.tab {
    border-right: 1px dashed var(--pri-border-color);
    display: flex;
    flex-direction: column;
    align-items: center;
}

.tab>img {
    max-width: 155px;
    max-height: 155px;
    aspect-ratio: 1;
    border-radius: 100%;
    margin-bottom: 15px;
}

.tab>ul {
    border-top: 1px dashed var(--pri-border-color);
    margin: 0;
    width: 100%;
}

.tab>ul>li {
    padding: 8px 0;
}

.tab>ul li>a {
    color: var(--sec-btn-color);
    width: 100%;
    display: flex;
    align-items: center;
    position: relative;
}

.tab>ul>li>a>svg:first-child {
    margin-right: 5px;
    width: 15px;
}

.tab>ul>li>a>svg:nth-child(2) {
    position: absolute;
    right: 0;
}

.with_sub ul {
    padding: 0;
    transition: initial;
}

.with_sub ul a {
    padding: 5px;
    padding-left: 25px;
}
</style>
<script>
export default {
    mounted() {
        $(".with_sub > ul").css({ display: "none" })
        $(".with_sub > a").click((e) => {
            var node = e.currentTarget;
            e.preventDefault()
            $(node).next().animate({ height: "toggle" })
            if (node.querySelector("svg:nth-child(2)").style.transform == "rotate(180deg)") {
                node.querySelector("svg:nth-child(2)").style.transform = "";
            }
            else {
                node.querySelector("svg:nth-child(2)").style.transform = "rotate(180deg)";
            }
        })
    }
}
</script>