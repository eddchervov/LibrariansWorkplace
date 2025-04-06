<template>
  <table class="table table-hover">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Название</th>
        <th scope="col">Автор</th>
        <th scope="col">Год публикации</th>
        <th scope="col">Количество копий</th>
        <th scope="col">Количество доступных книг <br />для выдачи</th>
        <th></th>
      </tr>
    </thead>
    <tbody>
      <tr colspan="6" v-if="$store.getters.availableBooks.length == 0">
        Нет книг для выдачи
      </tr>
      <tr v-for="(book, index) in $store.getters.availableBooks" :key="book.id">
        <th scope="row">{{ index + 1 }}</th>
        <td>{{ book.name }}</td>
        <td>{{ book.author }}</td>
        <td>{{ book.yearPublication }}</td>
        <td>{{ book.countCopies }}</td>
        <td>{{ book.countAvailableBooks }}</td>
        <td>
          <template
            v-if="book.countAvailableBooks.length === 0"
          >
            <span class="text-danger">Нет книг для выдачи</span>
          </template>
          <template v-else>
            <button
              class="btn btn-sm btn-secondary"
              @click="clickGiveBookToReader(book)"
            >
              Выдать книгу
            </button>
          </template>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script lang="ts">
import { GetAvailableBooksDto } from "@/models/dto/get-available-books-dto"
import { defineComponent } from "vue"

export default defineComponent({
  name: "AvailableBooks",
  components: {},
  data() {
    return {}
  },
  methods: {
    clickGiveBookToReader(book: GetAvailableBooksDto) {
      this.$store.commit("setSelectedGiveBookToReaderBook", book)
      this.setTab()
    },
    setTab() {
      this.$store.commit("setBooksTab", "GiveBookToReader")
    },
  },
})
</script>
