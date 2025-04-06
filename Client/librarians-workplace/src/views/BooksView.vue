<template>
  <div>
    <h4>Работа с книгами</h4>
    <hr />

    <template v-if="!isLoadForm">
      <template
        v-if="
          $store.state.booksTab === 'AvailableBooks' ||
          $store.state.booksTab === 'IssuedBooks'
        "
      >
        <div class="row">
          <div class="col-12">
            <div class="d-grid gap-2 d-md-block">
              <button
                class="btn btn-primary me-3"
                type="button"
                @click="$store.commit('setBooksTab', 'AvailableBooks')"
                :disabled="$store.state.booksTab === 'AvailableBooks'"
              >
                Доступные для выдачи книги
              </button>
              <button
                class="btn btn-primary"
                type="button"
                @click="$store.commit('setBooksTab', 'IssuedBooks')"
                :disabled="$store.state.booksTab === 'IssuedBooks'"
              >
                Выданные книги
              </button>
            </div>
          </div>
        </div>
      </template>

      <template v-if="$store.state.booksTab === 'AvailableBooks'">
        <AvailableBooks />
      </template>
      <template v-else-if="$store.state.booksTab === 'IssuedBooks'">
        <IssuedBooks />
      </template>
      <template v-else-if="$store.state.booksTab === 'GiveBookToReader'">
        <GiveBookToReader />
      </template>
      <template v-else-if="$store.state.booksTab === 'TakeBookFromReader'">
        <TakeBookFromReader />
      </template>
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
import AvailableBooks from "../components/books/AvailableBooks.vue"
import IssuedBooks from "../components/books/IssuedBooks.vue"
import GiveBookToReader from "../components/books/GiveBookToReader.vue"
import TakeBookFromReader from '../components/books/TakeBookFromReader.vue'

export default defineComponent({
  name: "BooksView",
  components: {
    AvailableBooks,
    IssuedBooks,
    GiveBookToReader,
    TakeBookFromReader,
  },
  data() {
    return {
      isLoadForm: false,
    }
  },
  methods: {
    setTab() {
      this.$store.commit("setBooksTab", "AvailableBooks")
    },
  },
  async mounted(): Promise<void> {
    await this.$store.dispatch("loadBooksPage")
    this.setTab()
    this.isLoadForm = false
  },
})
</script>
