# build stage
FROM node:22.14.0 as build-stage
WORKDIR /app
COPY Client/librarians-workplace/package*.json ./
RUN npm install
COPY Client/librarians-workplace .
RUN npm run build

# production stage
FROM nginx:stable-alpine as production-stage
COPY --from=build-stage /app/dist /usr/share/nginx/html
EXPOSE 1325
CMD ["nginx", "-g", "daemon off;"]
