<template>
  <div id="app">
    <nav
      id="nav"
      class="navbar navbar-expand-lg navbar-light bg-light py-2 mb-4"
    >
      <div class="container">
        <router-link to="/" class="navbar-brand"> Главная </router-link>
        <button
          class="navbar-toggler"
          type="button"
          data-bs-toggle="collapse"
          data-bs-target="#navbarText"
          aria-controls="navbarText"
          aria-expanded="false"
          aria-label="Toggle navigation"
        >
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarText">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            <li class="nav-item">
              <router-link to="/books" class="nav-link"> Книги </router-link>
            </li>
            <li class="nav-item">
              <router-link to="/book-management" class="nav-link"> Управление книгами </router-link>
            </li>
          </ul>
        </div>
      </div>
    </nav>

    <div class="container">
      <div class="row">
        <div class="col-12">
          <router-view />
        </div>
      </div>
    </div>


    <footer class="border-top footer text-muted mt-4">
      <div class="container">© 2025 - Рабочее место библиотекаря</div>
    </footer>
  </div>
</template>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  color: #2c3e50;
}

#nav {
  padding: 30px;
}

#nav a {
  font-weight: bold;
  color: #2c3e50;
}

#nav a.router-link-exact-active {
  color: #41b883;
  font-weight: 600 !important;
}

.fw-550 {
  font-weight: 550;
}
</style>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";

@Component({
  computed: {},
  components: {},
})
export default class App extends Vue {
  // mounted(): void {
  // }
}
</script>
