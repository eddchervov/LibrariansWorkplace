<template>
  <div>
    <h4>Управление книгами</h4>
    <hr />



    <template v-if="!isLoadForm">
      <div v-if="$store.state.bookManagementTab === 'list'">
        <BookOptions />
      </div>
      <div
        v-if="
          $store.state.bookManagementTab === 'create' ||
          $store.state.bookManagementTab === 'update'
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
import { defineComponent } from "vue"
import BookOptions from "../components/book-managements/BookOptions.vue"
import CreateUpdateBook from "../components/book-managements/CreateUpdateBook.vue"

export default defineComponent({
  name: "BookManagementView",
  components: {
    BookOptions,
    CreateUpdateBook,
  },
  data() {
    return {
      isLoadForm: true,
    }
  },
  async mounted(): Promise<void> {
    this.$store.commit("setBookManagementTab", "list")
    await this.$store.dispatch("loadBookManagement")
    this.isLoadForm = false
  },
})
</script>
