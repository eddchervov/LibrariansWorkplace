<template>
  <div>
    <h4>Управление книгами {{ $store.getters.bookManagementTab }}</h4>
    <hr />

<button class="btn btn-sm btn-secondary" >Добавить книгу</button>

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
import { Options, Vue } from 'vue-class-component'
import BookOptions from '../components/book-managements/BookOptions.vue'
import CreateUpdateBook from '../components/book-managements/CreateUpdateBook.vue'

@Options({
  components: {
    BookOptions,
    CreateUpdateBook
  }
})
export default class BookManagement extends Vue {
  isLoadForm = true

  async mounted (): Promise<void> {
    this.$store.commit('setBookManagementTab', 'list')
    await this.$store.dispatch('loadBookManagement')
    this.isLoadForm = false
  }
}
</script>
