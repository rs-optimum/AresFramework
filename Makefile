
.PHONY: build
build: 
	dotnet clean
	dotnet restore
	dotnet build --no-restore

.PHONY: publish
publish:
	dotnet publish "AresFramework.GameEngine/AresFramework.GameEngine.csproj" -c Release -o publish

.PHONY: test
test:
	dotnet test --no-build

.PHONY: docker-build
docker-build:
	sudo docker build . -t rsoptimum/aresframework-gameengine:$(BUILD_VERSION)

# This will run the tests we need
.PHONY: docker-test
docker-test:
	sudo docker build -f Test.Dockerfile -t test . --no-cache

.PHONY: docker-publish
docker-publish:
	sudo docker login -u rsoptimum -p $(DOCKER_PASSWORD)	
	sudo docker push rsoptimum/aresframework-gameengine:$(BUILD_VERSION)


.PHONY: docker-build-and-publish
docker-build-and-publish:
	make docker-build BUILD_VERSION=$(BUILD_VERSION)
	make docker-publish BUILD_VERSION=$(BUILD_VERSION) DOCKER_PASSWORD=$(BUILD_VERSION)
