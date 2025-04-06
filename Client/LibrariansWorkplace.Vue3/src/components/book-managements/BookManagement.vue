<template>
  <div>
    <h4>Управление книгами {{$store.getters.bookManagementTab}}</h4>
    <hr />

    <template v-if="!isLoadForm">
      <div v-if="$store.getters.bookManagementTab === 'list'">
        <BookOptions />
      </div>
      <div
        v-if="
          $store.getters.bookManagementTab === 'create' ||
          $store.getters.bookManagementTab === 'update'
        "
      >
        <CreateUpdateBook />
      </div>
    </template>

    <template v-else>
      <div class="row">
        <div class="col-12 text-center">
          <span class="text-primary">Загрузка...</span>
        </div>
      </div>
    </template>
  </div>
</template>

<script lang="ts">
import { Component, Vue } from "vue-property-decorator";
import BookOptions from "../book-managements/BookOptions.vue";

@Component({
  filters: {},
  components: { BookOptions },
})
export default class BookManagement extends Vue {
  isLoadForm = true;
  //tab: "list" | "create" | "update" = "list";

  async mounted(): Promise<void> {
    //this.$store.commit("setBookManagementTab", "list");
    await this.$store.dispatch("loadBookManagement");
    this.isLoadForm = false;
  }
}
</script>
