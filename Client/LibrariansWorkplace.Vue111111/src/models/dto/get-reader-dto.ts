import { BookGivenToReaderDto } from "./book-given-to-reader-dto";

export interface GetReaderDto {
  id: string;
  fullName: string;
  dateBirth: Date;
  issuedBooks: BookGivenToReaderDto[];
}
