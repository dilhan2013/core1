# My Docker experiments....

#Useful commands
##Docker stack deploy

To deploy complex multi-container apps, you can use the docker stack deploy command. You can either deploy a bundle on your machine over an SSH tunnel, or copy the docker-compose.yml file (for example using scp) to a manager node, SSH into the manager and then run docker stack deploy (if you have multiple managers, ensure that your session is on one that has the stack file)

`docker stack deploy -c docker-compose.yml testlab`

## Expose ports
By default, apps deployed with stacks do not have ports publicly exposed. Update port mappings for services, and Docker automatically wires up the underlying platform load balancers:

`docker service update --publish-add published=80,target=80 <example-service>`



## Linux Commands

### Run as root
`sudo -s`


#Azure Plugin

https://docs.docker.com/docker-for-azure/#docker-community-edition-ce-for-azure
