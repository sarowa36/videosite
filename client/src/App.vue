<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>

<template>
<header class="_navbar">
  <div class="container">
    <div class="row justify-content-between">
    <div class="col-3 logo">
      <img src="@/assets/logo.png" alt="">
    </div>
    <div class="col-6">
      <ul class="listofpages">
        <li>
          <a href="#">Top 50</a>
        </li>
        <li>
          <a href="#">Most Liked</a>
        </li>
        <li>
          <a href="#">Random</a>
        </li>
        <li>
          <a href="#">
            <FontAwesomeIcon icon="user"></FontAwesomeIcon>
          </a>
        </li>
      </ul>
    </div>
  </div>
</div>
</header>
  <RouterView />
</template>

<style scoped>
._navbar{
  background-color: var(--pri-color);
}
._navbar > .container > .row{
  height:70px;
}
.logo{
  height: 100%;
}
.logo > img{
  padding: 7px 0;
}
.listofpages{
  list-style: none;
  height: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  align-items: center;
}
</style>
