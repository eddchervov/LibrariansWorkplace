<template>
  <div>
    <h4>Список книг</h4>
    <hr />

    <template v-if="!isLoadForm">
      <div class="row">
        <div class="col-12">
          <div class="d-grid gap-2 d-md-block">
            <button
              class="btn btn-primary me-3"
              type="button"
              @click="tab = 'availableBooks'"
              :disabled="tab == 'availableBooks'"
            >
              Доступные для выдачи книги
            </button>
            <button
              class="btn btn-primary"
              type="button"
              @click="tab = 'issuedBooks'"
              :disabled="tab == 'issuedBooks'"
            >
              Выданные книги
            </button>
          </div>
        </div>
      </div>

      <template v-if="tab === 'availableBooks'">
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
            <tr
              v-for="(book, index) in $store.getters.availableBooks"
              :key="book.id"
            >
              <th scope="row">{{ index + 1 }}</th>
              <td>{{ book.name }}</td>
              <td>{{ book.author }}</td>
              <td>{{ book.yearPublication }}</td>
              <td>{{ book.countCopies }}</td>
              <td>{{ book.countAvailableBooks }}</td>
              <td>
                <div class="btn-group">
                  <button
                    type="button"
                    class="btn btn-secondary dropdown-toggle"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    Действие
                  </button>
                  <ul class="dropdown-menu">
                    <li><a class="dropdown-item" href="#">Действие</a></li>
                    <li>
                      <a class="dropdown-item" href="#">Другое действие</a>
                    </li>
                    <li>
                      <a class="dropdown-item" href="#">Что-то еще здесь</a>
                    </li>
                    <li><hr class="dropdown-divider" /></li>
                    <li>
                      <a class="dropdown-item" href="#">Отделенная ссылка</a>
                    </li>
                  </ul>
                </div>
              </td>
            </tr>
          </tbody>
        </table>
      </template>
      <template v-else-if="tab === 'issuedBooks'">
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
            <tr
              v-for="(book, index) in $store.getters.issuedBooks"
              :key="book.id"
            >
              <th scope="row">{{ index + 1 }}</th>
              <td>{{ book.name }}</td>
              <td>{{ book.author }}</td>
              <td>{{ book.yearPublication }}</td>
              <td>{{ book.countCopies }}</td>
              <td>{{ book.countIssuedBooks }}</td>
              <td></td>
            </tr>
          </tbody>
        </table>
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
import { Options, Vue } from 'vue-class-component'

export default class Books extends Vue {
  msg!: string
  isLoadForm = false
  tab: 'issuedBooks' | 'availableBooks' = 'availableBooks';

  async mounted (): Promise<void> {
    await this.$store.dispatch('loadBooksPage')
    this.isLoadForm = false
  }
}
</script>
