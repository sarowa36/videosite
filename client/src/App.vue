<script setup>
import { RouterLink, RouterView } from 'vue-router'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome';
</script>

<template>
  <header class="_navbar">
    <div class="container">
      <div class="row justify-content-between">
        <div class="col-4 logo">
          <RouterLink to="/"><img src="@/assets/logo.png" alt=""></RouterLink>
        </div>
        <div class="col-3 d-lg-none d-flex justify-content-center">
          <button class="mobile_btn" @click="mobileSub">
            <span></span>
            <span></span>
            <span></span>
          </button>
        </div>
        <div class="col-lg-7 align-items-center justify-content-end">
          <ul class="listofpages m-0 p-0">
            <li>
              <router-link to="/">HomePage</router-link>
            </li>
            <li>
              <router-link to="/Topics">Top 50</router-link>
            </li>
            <li>
              <router-link to="/MostLiked">Most Liked</router-link>
            </li>
            <li>
              <router-link to="/Random">Random</router-link>
            </li>
            <li>
              <router-link to="/User/Edit">
                <FontAwesomeIcon icon="user"></FontAwesomeIcon>
              </router-link>
              <div class="submenu">
                
              </div>
            </li>
          </ul>
        </div>
      </div>
    </div>
  </header>
  <main>
    <RouterView />
  </main>
  <footer>
    <div class="container">
      <div class="row pt-4 align-items-center">
        <div class="col-md-4 col-lg-3 footer-logo">
          <router-link to="/"><img src="@/assets/logo.png" alt=""></router-link>
          <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec nibh nisi, scelerisque at lacinia id, euismod
            et mauris. Morbi ac vestibulum nisi. Aliquam urna risus, vestibulum et magna non, </p>
        </div>
        <div class="col-md-8 col-lg-6">
          <h4>Categories</h4>
          <ul class="list_of_tags">
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
            <li><a href="#"><font-awesome-icon :icon="['fas', 'tag']" />Lorem</a></li>
          </ul>
        </div>
        <div class="col-md-4 col-lg-3 quick_menu_parent">
          <h4>Quick Menu</h4>
          <ul class="quick_menu p-0">
            <li><router-link to="/">Homepage</router-link></li>
            <li><router-link to="/Topics">Top 50</router-link></li>
            <li><router-link to="/MostLiked">Most Liked</router-link></li>
            <li><router-link to="/Random">Random</router-link></li>
            <li><router-link to="/User/Login">Login</router-link></li>
            <li><router-link to="/User/Register">Register</router-link></li>
          </ul>
        </div>
      </div>
    </div>
    <div class="container-fluid mt-3">
      <div class="row">
        <div class="col-12 page_bottom_bar">
          Copyright Reserved By <a href="https://github.com/sarowa36">sarowa36</a>
        </div>
      </div>
    </div>
  </footer>
</template>
<script>
export default {
  data() {
  },
  methods: {
    mobileSub() {
      $(".listofpages").parent().animate({ height: "toggle" })
    }
  },
  mounted() {
    if (document.body.offsetWidth < 992) {
      document.querySelector(".listofpages").parentElement.setAttribute("style", "display: none; transition: initial;")
    }
  }
}
</script>
<style scoped>
._navbar,
footer {
  background-color: var(--pri-color);
}

main {
  min-height: 70vh;
}

.logo {
  height: 100%;
}

.logo img {
  padding: 7px 0;
  height: 70px;
  object-fit: contain;
}

.listofpages {
  list-style: none;
  height: 100%;
  display: flex;
  flex-direction: row;
  justify-content: space-evenly;
  align-items: center;
}

.footer-logo {
  display: flex;
  flex-direction: column;
  align-items: center;
  font-size: 13px;
}

.footer-logo img {
  width: 130px;
  object-fit: contain;
  margin-bottom: 15px;
}

.list_of_tags {
  display: flex;
  flex-wrap: wrap;
  padding: 0;
  justify-content: center;
}

.list_of_tags>li {
  margin-right: 20px;
}

.list_of_tags>li>a {
  color: var(--sec-btn-color);
  font-size: 15px;
}

.list_of_tags>li>a>svg {
  margin-right: 5px;
}

.quick_menu a {
  color: var(--sec-btn-color);
}

.page_bottom_bar {
  text-align: center;
  padding: 20px 0;
  border-top: 0.5px solid var(--pri-border-color);
}

.mobile_btn>span {
  width: 24px;
  display: block;
  border: 1px solid var(--pri-t-color);
  margin-bottom: 5px;
  border-radius: 10%;
}

.mobile_btn>span:last-child {
  margin: 0;
}

@media (min-width:992px) {
  .listofpages>li {
    margin-right: 30px;
  }
}

@media (max-width: 992px) {
  .listofpages {
    flex-direction: column;
    width: 100%;
    background-color: var(--pri-color);
    overflow: hidden;
  }

  .listofpages li,
  .listofpages li a {
    width: 100%;
  }

  .listofpages li a {
    display: block;
    text-align: center;
    padding: 5px 0;
  }
}

@media (max-width:768px) {
  .quick_menu_parent {
    text-align: center;
  }
}</style>
