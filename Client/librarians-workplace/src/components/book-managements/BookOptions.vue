<template>
  <div>
    <button class="btn btn-sm btn-secondary mb-3" @click="addBook()">Добавить книгу</button>

    <table class="table table-hover">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Название</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr colspan="6" v-if="$store.state.bookOptions.length == 0">
          Нет книг
        </tr>
        <tr v-for="(book, index) in $store.state.bookOptions" :key="book.id">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ book.name }}</td>
          <td>
            <button
              class="btn btn-sm btn-danger float-end"
              :disabled="btnDeleteBook"
              @click="deleteBook(book)"
            >
              Удалить
            </button>
            <button
              class="btn btn-sm btn-secondary float-end me-2"
              @click="updateBook(book)"
            >
              Редактировать
            </button>
          </td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script lang="ts">
import { defineComponent } from "vue"
import { BookOptionDto } from "@/models/dto/books/book-option-dto"

export default defineComponent({
  name: "BookOptions",
  components: {},
  data() {
    return {
      btnDeleteBook: false
    }
  },
  methods: {
    addBook(): void {
      this.$store.commit("setSelectedBookOption", null)
      this.$store.commit("setBookManagementTab", "create")
    },
    updateBook(book: BookOptionDto): void {
      this.$store.commit("setSelectedBookOption", book)
      this.$store.commit("setBookManagementTab", "update")
    },
    async deleteBook(book: BookOptionDto): Promise<void> {
      const result = confirm(`Удалить книгу ${book.name}?`)
      if (!result) return
      this.btnDeleteBook = true
      await this.$store.dispatch("deleteBook", book.id)
      this.btnDeleteBook = false
    },
  },
})
</script>
