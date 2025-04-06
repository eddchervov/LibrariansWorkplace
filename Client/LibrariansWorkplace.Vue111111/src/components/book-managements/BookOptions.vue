<template>
  <div>
    <table class="table table-hover">
      <thead>
        <tr>
          <th scope="col">#</th>
          <th scope="col">Название</th>
          <th></th>
        </tr>
      </thead>
      <tbody>
        <tr colspan="6" v-if="$store.getters.bookOptions.length == 0">
          Нет книг
        </tr>
        <tr v-for="(book, index) in $store.getters.bookOptions" :key="book.id">
          <th scope="row">{{ index + 1 }}</th>
          <td>{{ book.name }}</td>
          <td>
            <button
              class="btn btn-sm btn-secondary float-end"
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
import { Options, Vue } from 'vue-class-component'
import { BookOptionDto } from '@/models/dto/books/book-option-dto'

export default class BookOptions extends Vue {
  updateBook (book: BookOptionDto): void {
    this.$store.commit('setSelectedBookOption', book)
    this.$store.commit('setBookManagementTab', 'update')
  }
}
</script>
