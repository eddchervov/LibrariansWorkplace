<template>
  <table class="table table-hover">
    <thead>
      <tr>
        <th scope="col">#</th>
        <th scope="col">Название</th>
        <th scope="col">Автор</th>
        <th scope="col">Год публикации</th>
        <th scope="col">Количество копий</th>
        <th scope="col">Количество выданных книг</th>
        <th></th>
      </tr>
    </thead>
    <tbody>
      <tr colspan="6" v-if="$store.getters.issuedBooks.length == 0">
        Нет выданных книг
      </tr>
      <tr v-for="(book, index) in $store.getters.issuedBooks" :key="book.id">
        <th scope="row">{{ index + 1 }}</th>
        <td>{{ book.name }}</td>
        <td>{{ book.author }}</td>
        <td>{{ book.yearPublication }}</td>
        <td>{{ book.countCopies }}</td>
        <td>{{ book.countIssuedBooks }}</td>
        <td>
          <button
            class="btn btn-sm btn-secondary"
            @click="clickTakeBookFromReader(book)"
          >
            Принять книгу
          </button>
        </td>
      </tr>
    </tbody>
  </table>
</template>

<script lang="ts">
import { GetIssuedBooksDto } from "@/models/dto/get-issued-books-dto"
import { defineComponent } from "vue"

export default defineComponent({
  name: "IssuedBooks",
  components: {},
  data() {
    return {}
  },
  methods: {
    clickTakeBookFromReader(book: GetIssuedBooksDto) {
      this.$store.commit("setSelectedBookFromTabTakeBookFromReader", book)
      this.setTab()
    },
    setTab() {
      this.$store.commit("setBooksTab", "TakeBookFromReader")
    },
  },
})
</script>
