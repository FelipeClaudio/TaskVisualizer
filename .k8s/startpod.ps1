kubectl apply -f .\taskvisualizerweb.yaml;
kubectl port-forward pod/taskvisualizerweb 32123:8080